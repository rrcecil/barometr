namespace Barometr.Controllers {

    export class ProfileController {

        public profile;
        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService) {
            $http.get(`api/profiles`).then((res) => {
                this.profile = res.data;
            });
        }

    }

    export class ProfileReviewsController {
        public drinkReviews;
        public barReviews;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService) {
            $http.get(`api/DrinkReviews/myDrinkReviews`).then((res) => {
                this.drinkReviews = res.data;
                console.log("My drink reviews: " + this.drinkReviews);

            });
            $http.get(`api/BarReviews/myBarReviews`).then((res) => {
                this.barReviews = res.data;
                console.log("My bar reviews: " + this.barReviews);
            });
        }
    }
}


