namespace Barometr.Controllers {

    const apiURL = '/api/bars';

    export class HomeController {
        public randomBar;
        

        //constructor(private RandomBarService: Barometr.Services.RandomBarService) {
        //    let randomBar = RandomBarService.randomBar;
        //    console.log("Random Bar " + randomBar);
        //    this.randomBar = randomBar;
        //}
    }


    export class BarController {
        public bars;
        public drinks;
        public reviews;


        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService, public $uibModal: angular.ui.bootstrap.IModalService) {
            $http.get(`/api/bars/${$stateParams['id']}`).then((res) => {
                this.bars = res.data;
            });
            $http.get(`api/bars/drinks`).then((res) => {
                this.drinks = res.data;

            });
        }

        public openReviewDialog() {
            this.$uibModal.open({
                templateUrl: 'ngApp/views/reviewdialog.html',
                controller: 'ReviewDialogController',
                controllerAs: 'modal',
                resolve: {
                    bars: () => this.bars
                },
                size: 'lg'
            });
        }

        public openDrinkDialog() {
            this.$uibModal.open({
                templateUrl: 'ngApp/views/drinkdialog.html',
                controller: 'DrinkDialogController',
                controllerAs: 'modal',
                resolve: {
                    drinks: () => this.drinks
                },
                size: 'sm'
            });
        }

        public claimBar(id) {
            console.log(id);
            this.$http.post(`api/requests/` + id, id).then((res) => {
            });
        }
    }

    export class UserMetricController {
        public barReviewCount;
        public drinkReviewCount;
        public randomBar;
        public randomDrink;
        public drinks;
        public greetings;
        public requests;
        public showRequestNum;



        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService,
            public RandomBarService: Barometr.Services.RandomBarService
        ) {
            $http.get(`api/UserMetric/barReviewCount`).then((res) => {
                this.barReviewCount = res.data;

                console.log("the Bar Review count is " + this.barReviewCount);
            })
                .catch((response) => {
                    console.error("did not work");
                });

            $http.get(`api/UserMetric/drinkReviewCount`).then((res) => {
                this.drinkReviewCount = res.data;
                console.log("the Drink Review count is " + this.drinkReviewCount);
            });

            $http.get(`api/bars/drinks`).then((res) => {
                this.drinks = res.data;
                console.log(res.data);
                console.log(this.drinks);

            });

            $http.get(`/api/drinks/randomDrink`).then((res) => {
                console.log("the drink is " + res.data);
                this.randomDrink = res.data;
            });

           

            this.showRequestNum = true;
        }

        public greet() {
            var myDate = new Date();
            var hrs = myDate.getHours();



            if (hrs < 12)
                return 'Good Morning';
            else if (hrs >= 12 && hrs <= 17)
                return 'Good Afternoon';
            else if (hrs >= 17 && hrs <= 24)
                return 'Good Evening';

        }

        public getRequests(admin) {
            if (admin != null) {
                this.$http.get(`api/requests/amount`).then((res) => {
                    this.requests = res.data;
                });
            }
        }

        public goToRequests() {
            this.showRequestNum = false;
            this.$state.go("requests");
        }
    }

    angular.module('Barometr').controller('UserMetricController', UserMetricController);



    export class ReviewDialogController {
        public reviews;
        public barId;
        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService, public $uibModal: angular.ui.bootstrap.IModalService, public $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, public bars) {
            $http.get('api/barReviews').then((res) => {
                this.reviews = res.data;
                console.log(this.bars);
            });
            this.barId = this.$stateParams['id'];
        }

        public postReview(review) {
            review.barId = this.barId;
            this.$http.post(`api/barReviews`, review).then((res) => {
                this.$state.reload();
            });
        }
        public updateReview(review) {
            this.$http.put(`api/bars/${review.id}`, review).then((res) => {
                this.$state.reload();
            });
        }
        public deleteReview(review) {
            this.$http.delete(`api/bars/reviews/${review.id}`).then((res) => {
                this.$state.reload();
            });
        }
        public closeModal() {
            this.$uibModalInstance.close();
        }
    }


    angular.module('Barometr').controller('ReviewDialogController', ReviewDialogController);


    export class DrinkDialogController {
        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $uibModal: angular.ui.bootstrap.IModalService, public $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, public drink) { }
        public postDrink(drink) {
            this.$http.post('api/drinks', drink).then((res) => {
                this.$state.reload();
            });
        }
        public updateDrink(drink) {
            this.$http.put(`api/drinks/${drink.id}`, drink).then((res) => {
                this.$state.reload();
            });
        }
        public deleteDrink(drink) {
            this.$http.delete(`api/drinks/${drink.id}`).then((res) => {
                this.$state.reload();
            });
        }
        public closeModal() {
            this.$uibModalInstance.close();
        }
    }
    angular.module('Barometr').controller('DrinkDialogController', DrinkDialogController);

    export class DrinkController {
        public bars;
        public randomDrink;

        constructor(public $http: ng.IHttpService, public $stateParams: ng.ui.IStateParamsService, public $state: ng.ui.IStateService, private accountService: Barometr.Services.AccountService) {
            $http.get(`api/bars/drinks`).then((res) => {
                this.bars = res.data;
            });

            $http.get(`/api/bars/randomDrink`).then((res) => {
                console.log(res.data);
                this.randomDrink = res.data;
            });
        }



        //    public postDrink(drink) {
        //        this.$http.post('api/bars/drinks', drink).then((res) => {
        //            this.$state.reload();
        //        });
        //    }
        //    public deleteDrink(drink) {
        //        this.$http.delete(`api/bars/drinks/${drink.id}`).then((res) => {
        //            this.$state.reload();
        //        });
        //    }
        //    public updateDrink(drink) {

        //        this.$http.put(`api/bars/drinks/${drink.id}`, drink).then((res) => {
        //            this.$state.reload();
        //        });

        //    }
        //}

        //export class ReviewController {
        //    public bar;
        //    public drink;

        //    constructor(public $http: ng.IHttpService, public $stateParams: ng.ui.IStateParamsService, public $state: ng.ui.IStateService, private accountService: Barometr.Services.AccountService) {
        //        $http.get(`api/bars/reviews`).then((res) => {
        //            this.bar = res.data;
        //            console.log(this.bar);
        //        });

        //    }
        //    public postReview(review) {
        //        this.$http.post(`api/bars/reviews`, review).then((res) => {
        //            this.$state.reload();
        //        });
        //    }

        //    public deleteReview(review) {
        //        this.$http.delete(`api/bars/reviews/${review.id}`).then((res) => {
        //            this.$state.reload();
        //        });
        //    }
        //    public updateReview(review) {

        //        this.$http.put(`api/bars/reviews/${review.id}`, review).then((res) => {
        //            this.$state.reload();
        //        });

        //    }
        //    public validateUser(review) {
        //        if (this.accountService.getUserName() != review.userName) {
        //            return true;
        //        }
        //        else {
        //            return false;
        //        }
        //    }
    }

    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }


    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
