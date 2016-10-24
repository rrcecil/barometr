namespace Barometr.Controllers {

    export class RequestsController {
        public requests;
        public request;

        constructor(public $http: ng.IHttpService, public $state: ng.ui.IStateService) {
            $http.get(`api/requests`).then((res) => {
                this.requests = res.data;
            });

            
        }

        public accept(id) {
            console.log(id);
            this.$http.post(`api/requests/accept/` + id, id).then((res) => {
                this.$state.reload();
                alert("The user's request for ownership of this business has been accepted.");
            });
        }

        public deny(id) {
            this.$http.post(`api/requests/deny/` + id, id).then((res) => {
                this.$state.reload();
                alert("The user's requested for ownership of this business has been denied.");
            });
        }

        public claimBar(id) {
            this.$http.post(`api/account/claimbusiness/`, id).then((res) => {
            });
        }
    }

}