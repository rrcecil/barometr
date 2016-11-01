namespace Barometr.Controllers {

    export class MenuController {

        public drinks;
        public bar;
        public drink;
        public drinkReviews;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $uibModal: angular.ui.bootstrap.IModalService) {
            $http.get(`api/drinks/menu`).then((res) => {
                this.drinks = res.data;
            });

            $http.get(`api/bars/bar`).then((res) => {
                this.bar = res.data;
            });
           

}

    }

    export class AddToMenuController {
        public drink;
        public drinks;
        public drinkReviews;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $uibModal: angular.ui.bootstrap.IModalService
        ) {
            $http.get(`api/drinks`).then((res) => {
                this.drinks = res.data;
            });
            $http.get(`api/DrinkReviews/DrinkReviews`).then((res) => {
                this.drinkReviews = res.data;
                console.log("Drink reviews: " + res.data);

            });
        }


        public add(drink) {
            console.log(drink);
            this.$http.post(`api/drinks/addto`, drink).then((res) => {
                this.$state.reload();
            });
        }


        public delete(drinkId) {
            this.$http.delete(`api/drinks/${drinkId}`).then((res) => {
                this.$state.reload();
            });
        }

        public showModal() {
            this.$uibModal.open({
                templateUrl: '/ngApp/views/dialogMenu.html',
                controller: 'DialogMenuController',
                controllerAs: 'modal',
                resolve: {
                    drink: () => this.drink
                },
                size: 'md'
            });
        }


        public openDrinkDialog() {
            this.$uibModal.open({
                templateUrl: 'ngApp/views/drinkDialog.html',
                controller: 'DialogMenuController',
                controllerAs: 'modal',
                resolve: {
                    drink: () => this.drink
                },
                size: 'md'
            });
        }

    }

    export class DialogMenuController {
        public drink;
        public drinkReviews;

        constructor(private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, public $http: ng.IHttpService, public $stateParams: ng.ui.IStateParamsService, public $state: ng.ui.IStateService) {
        }

        public add() {
            this.$uibModalInstance.close();
        }

        public ok() {
            console.log(this.drink);
            this.$http.post(`api/drinks`, this.drink).then((res) => {
                this.add();
                this.$state.reload();
            });
        }
        public closeModal() {
            this.$uibModalInstance.close();
        }

        public post(review) {
            console.log("drinkReviews");
            this.$http.post(`api/drinkReviews`, review).then((res) => {
                this.$state.reload();
            });
        }
    }
        angular.module("Barometr").controller('DialogMenuController', DialogMenuController);
}
