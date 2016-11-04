namespace Barometr.Controllers {

    const apiURL = '/api/bars';

    export class HomeController {
        public randomBar;
    }

    export class BarController {
        public bar;
        public drinks;
        public reviews;
        public rating;
        public map;
        public hours;
        public mapOptions;
        public result;
        public mapDiv;
        public photos;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService, public $uibModal: angular.ui.bootstrap.IModalService, public $scope: ng.IScope) {
            $http.get(`/api/bars/${$stateParams['id']}`).then((res) => {
                this.bar = res.data;
                this.rating = this.bar.rating;
            })
            .then(() => {
                this.mapOptions = {
                    center: new google.maps.LatLng(29.790128, -95.402796),
                    zoom: 13
                };
                this.result = document.getElementById('map');
                this.mapDiv = angular.element(this.result);
                this.map = new google.maps.Map(this.mapDiv[0], this.mapOptions);
                var service = new google.maps.places.PlacesService(this.map);

                service.getDetails({ placeId: this.bar['placeId'] }, (res) => {
                    $scope.$apply(() => {
                        this.hours = res['opening_hours']['weekday_text'];
                        this.photos = res.photos.map(item => item.getUrl({ maxHeight: 310, maxWidth: 350 }));
                    });
                    console.log(this.photos);
                    });
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
                    bar: () => this.bar
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
        public mapOptions;
        public map;
        public result;
        public mapDiv;



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
        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService, public $uibModal: angular.ui.bootstrap.IModalService, public $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, public bar) {
            $http.get('api/barReviews').then((res) => {
                this.reviews = res.data;
                console.log(this.bar);
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
        
        public drinks;

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
        public drink;

        constructor(public $http: ng.IHttpService, public $stateParams: ng.ui.IStateParamsService, public $state: ng.ui.IStateService, private accountService: Barometr.Services.AccountService) {
            $http.get(`api/bars/drinks`).then((res) => {
                this.bars = res.data;
            });

            $http.get(`/api/bars/randomDrink`).then((res) => {
                console.log(res.data);
                this.randomDrink = res.data;
            });
        }
    }
       

     

    export class DrinkReviewController {
        public drinkReviews;
        public drink;

        constructor(public $http: ng.IHttpService, public $stateParams: ng.ui.IStateParamsService, public $state: ng.ui.IStateService, private accountService: Barometr.Services.AccountService) {

            $http.get(`api/drinkReviews`).then((res) => {
                this.drinkReviews = res.data;
                console.log(this.drinkReviews);
            });

        }
        public post(review) {
            console.log("drinkReviews");
            this.$http.post(`api/drinkReviews`, review).then((res) => {
                this.$state.reload();
            });
    
            }

           

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
