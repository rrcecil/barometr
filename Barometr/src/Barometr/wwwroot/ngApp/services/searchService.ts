namespace Barometr.Services {
    export class SearchService {
        public displayMap;
        public bars;
        public map;

        constructor(public $q: ng.IQService, public $http: ng.IHttpService) {
            this.map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: 29.7604, lng: -95.3698 },
                zoom: 13
            });
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

            this.map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: 29.7604, lng: -95.3698 },
                zoom: 13
            });
            
            var deferred = this.$q.defer();
            var coords = this.getLatLong(zipCode);
            coords.then((res) => {
                var location = new google.maps.LatLng(res['lat'](), res['lng']());
                var service = new google.maps.places.PlacesService(this.map);
                var request = {
                    location: location,
                    radius: 7000,
                    types: ['bar']
                };
                this.map.setCenter(location);
                this.map.setZoom(12);
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
                        deferred.resolve(this.bars);
                    }
                });
            });
            return deferred.promise;

        }
    }
    angular.module('Barometr').service('SearchService', SearchService);
}