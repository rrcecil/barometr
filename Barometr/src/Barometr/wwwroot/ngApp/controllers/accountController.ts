namespace Barometr.Controllers {

    export class AccountController {
        public externalLogins;

        public getUserName() {
            return this.accountService.getUserName();
        }

        public getName() {
            return this.accountService.getName();
        }

        public getUserId() {
            return this.accountService.getUserName();
        }

        public getClaim(type) {
            return this.accountService.getClaim(type);
        }

        public isLoggedIn() {
            return this.accountService.isLoggedIn();
        }

        public logout() {
            this.accountService.logout();
            this.$location.path('/');
        }

        public getExternalLogins() {
            return this.accountService.getExternalLogins();
        }

        constructor(private accountService: Barometr.Services.AccountService, private $location: ng.ILocationService, public $state: ng.ui.IStateService) {
            this.getExternalLogins().then((results) => {
                this.externalLogins = results;
            });
        }

        public searchSubmit(zipcode) {
            this.$state.go('search', { zipcode: zipcode });

        }
    }

    angular.module('Barometr').controller('AccountController', AccountController);


    export class LoginController {
        public loginUser;
        public validationMessages;
        public prov; //facebook
        public Provider; //Facebook

        public login() {
            console.log(this.loginUser);
            this.accountService.login(this.loginUser).then(() => {
                this.$location.path('/landingPage');
            }).catch((results) => {
                this.validationMessages = results;
            });
        }

        public loginBtn(socialBtn) {
            console.log(socialBtn);
            if (socialBtn === "Facebook") {
                this.prov = "facebook";
                this.Provider = "Facebook";
            }
            else if (socialBtn === "Twitter") {
                this.Provider = "Twitter";
                this.prov = "twitter";
            }
            else {
                return null;
            }
        }


        constructor(private accountService: Barometr.Services.AccountService, private $location: ng.ILocationService) { }
    }


    export class RegisterController {
        public registerUser;
        public validationMessages;

        public register() {
            this.accountService.register(this.registerUser).then(() => {
                this.$location.path('/');
            }).catch((results) => {
                this.validationMessages = results;
            });
        }

        constructor(private accountService: Barometr.Services.AccountService, private $location: ng.ILocationService) { }
    }

    export class ExternalRegisterController {
        public registerUser;
        public validationMessages;

        public register() {
            this.accountService.registerExternal(this.registerUser.email)
                .then((result) => {
                    this.$location.path('/');
                }).catch((result) => {
                    this.validationMessages = result;
                });
        }
        constructor(private accountService: Barometr.Services.AccountService, private $location: ng.ILocationService) {}

    }

    export class ConfirmEmailController {
        public validationMessages;

        constructor(
            private accountService: Barometr.Services.AccountService,
            private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $location: ng.ILocationService
        ) {
            let userId = $stateParams['userId'];
            let code = $stateParams['code'];
            accountService.confirmEmail(userId, code)
                .then((result) => {
                    this.$location.path('/');
                }).catch((result) => {
                    this.validationMessages = result;
                });
        }
    }

}
