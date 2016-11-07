namespace Barometr.Controllers {
    export class UserBarController {
        public userBars;
        public mapOptions;
        public result;
        public mapDiv;
        public map;
        public photos;
       
        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $scope: ng.IScope) {

            $http.get(`api/favoriteBar/favoriteBars`).then((res) => {
                this.userBars = res.data;
                })
                .catch((response) => {
                    console.error("error");
                });
        }
       

    }
}
