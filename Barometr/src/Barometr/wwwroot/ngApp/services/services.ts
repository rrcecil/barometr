namespace Barometr.Services {
    export class RandomBarService {
        public randomBar;
        public map;
        public mapOptions;
        public result;
        public mapDiv;
        public hours;
        public details;
        public latitude;
        public longitude;


        constructor(private $http: ng.IHttpService) {
            this.$http.get(`/api/bars/random`).then((res) => {
                this.randomBar = res.data;
            })
                .then(() => {
                    this.mapOptions = {
                       
                    center: new google.maps.LatLng(this.randomBar.latitude, this.randomBar.longitude),
                    zoom: 12
                    };
                    this.result = document.getElementById('map');
                    this.mapDiv = angular.element(this.result);
                    this.map = new google.maps.Map(this.mapDiv[0], this.mapOptions);
                    var service = new google.maps.places.PlacesService(this.map);
                    var latitude = this.randomBar.latitude;
                    var longitude = this.randomBar.longitude;
                    var marker = new google.maps.Marker({
                        map: this.map,
                        position: new google.maps.LatLng(latitude, longitude),
                        animation: google.maps.Animation.DROP,
                        title: this.randomBar.name,
                    });
                });
        }            
    }
    angular.module('Barometr').service('RandomBarService', RandomBarService);
}



