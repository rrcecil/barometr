namespace Barometr.Controllers {

    export class RequestsController {
        public requests;
        public request;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService) {
            $http.get(`api/requests`).then((res) => {
                this.requests = res.data;
            });

            
        }
        public claimBar(id) {
            this.$http.post(`api/account/claimbusiness/`, id).then((res) => {
            });
        }
    }

}