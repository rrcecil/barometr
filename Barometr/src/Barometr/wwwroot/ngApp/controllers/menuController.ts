namespace Barometr.Controllers {

    export class MenuController {

        public drinks;
        public bar;
        public drink;

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

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService, public $uibModal: angular.ui.bootstrap.IModalService) {
            $http.get(`api/drinks`).then((res) => {
                this.drinks = res.data;
            });
        }

        public add(drink) {
            console.log(drink);
            this.$http.post(`api/drinks/addto`, drink).then((res) => {
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
    }

    export class DialogMenuController {
        public drink;

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
    }

    angular.module("Barometr").controller('DialogMenuController', DialogMenuController);

}
