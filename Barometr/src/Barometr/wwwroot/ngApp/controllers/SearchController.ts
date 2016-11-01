
namespace Barometr.Controllers {

    export class SearchController {
        public result;
        public bars;
        public map;
        public mapDiv;
        public lat;
        public lng;
        public geocode;
        public zip;
        public mapOptions;

        constructor(public ngGPlacesAPI, public $http: ng.IHttpService, public $q: ng.IQService, public $scope: ng.IScope) {
            this.displayMap();
        }

        public displayMap(location?) {
            this.mapOptions = {
                center: location || new google.maps.LatLng(29.790128, -95.402796),
                zoom: 13
            };
            this.result = document.getElementById('map');
            this.mapDiv = angular.element(this.result);
            this.map = new google.maps.Map(this.mapDiv[0], this.mapOptions);
        }

        public getLatLong(zipCode) {
            var deferred = this.$q.defer();
            var geocoder = new google.maps.Geocoder();

            //get zipcode from the view and pass it into geocode
            geocoder.geocode({ 'address': zipCode }, (res, stat) => {
                if (stat == google.maps.GeocoderStatus.OK) {
                    //assign response to lat and long
                    var result = res[0];
                    var geometry = result.geometry.location;
                    deferred.resolve(geometry);
                    return;
                } else {
                    deferred.reject("Couldn't find that address! Google Maps say: " + stat);
                }
            });
            return deferred.promise;
        }

        public getBars(zipCode) {

            var coords = this.getLatLong(zipCode);
            coords.then((res) => {
                var location = new google.maps.LatLng(res['lat'](), res['lng']());
                var service = new google.maps.places.PlacesService(this.map);
                var request = {
                    location: location,
                    radius: 7000,
                    types: ['bar']
                };
                this.displayMap(location);
                this.bars = [];
                
                service.nearbySearch(request, (res, stat) => {
                    if (stat == google.maps.places.PlacesServiceStatus.OK) {

                        res.forEach((res) => {
                            var bar = {
                                name: res.name,
                                googleBarId: res['id'],
                                latitude: res.geometry.location.lat(),
                                longitude: res.geometry.location.lng(),
                                placeId: res.place_id
                            };
                            
                            this.$http.post('api/bars', bar).then((res) => {
                                bar['Id'] = res.data['barId'];
                            });
                            bar['open_now'] = res['opening_hours'] ? res['opening_hours']['open_now'] : false;
                            
                            this.bars.push(bar);
                            var marker = new google.maps.Marker({ position: res.geometry.location, map: this.map });
                        });
                        this.$scope.$apply(() => {
                            this.bars;
                        });
                    }
                });
            });
        }

        public addBar(bar) {
            bar.disabled = true;
            this.$http.post('api/userBars', bar).then((res) => res);
        }
    }
}






