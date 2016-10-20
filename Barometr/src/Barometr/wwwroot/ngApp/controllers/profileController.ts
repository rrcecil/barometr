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
        public reviews;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService) {
            $http.get(`api/reviews/myReviews`).then((res) => {
                this.reviews = res.data;
            });
        }
    }
}


