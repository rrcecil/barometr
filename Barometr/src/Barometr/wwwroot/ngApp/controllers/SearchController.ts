
namespace Barometr.Controllers {

    export class SearchController {
        public result;
        public options;
        public neighborhoods;
        public bars;
        public map;
        public mapDiv;
        public lat;
        public lng;
        public geocode;
        public zip;
        public mapOptions;

        constructor(public ngGPlacesAPI, public $http:ng.IHttpService, public $q: ng.IQService) {
            this.options = {
                types: ['establishment']
            };
            this.neighborhoods = {
                heights: { latitude: 29.790128, longitude: -95.402796 },
                riceVillage: { latitude: 29.715253, longitude: -95.418873 },
                pearland: { latitude: 29.543877, longitude: -95.387624 }
            };

            this.displayMap();
        }

        public displayMap(lat?, lng?) {
            this.mapOptions = {
                center: new google.maps.LatLng(lat || 29.790128, lng || -95.402796),
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
                    deferred.resolve({ latitude: geometry.lat(), longitude: geometry.lng() });
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
                this.lat = res['latitude'];
                this.lng = res['longitude'];

                this.displayMap(this.lat, this.lng);

                this.ngGPlacesAPI.nearbySearch({ latitude: this.lat, longitude: this.lng }).then((res) => {
                    this.bars = "";
                    this.bars = res;

                    for (let bar of this.bars) {
                        bar.googleBarId = bar.id;
                        delete bar.id;
                        bar.latitude = bar.geometry.location.lat();
                        bar.longitude = bar.geometry.location.lng();
                        this.$http.post('api/bars', bar).then((res) => {
                            bar.Id = res.data['barId'];
                        });
                    }
                });
            });
        }

        public addBar(bar) {
            bar.disabled = true;
            
            this.$http.post('api/userBars', bar).then((res) => res );
        }
    }
}





