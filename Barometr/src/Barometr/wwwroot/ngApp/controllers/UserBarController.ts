namespace Barometr.Controllers {
    export class UserBarController {
        public userBars;
        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService) {

            $http.get(`api/userBars/userBars`).then((res) => {
                this.userBars = res.data;
                console.log(this.userBars);
            })
                .catch((response) => {
                    console.error("error");
                });
        }

    }
}
