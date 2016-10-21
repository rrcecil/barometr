namespace Barometr.Services {
    export class RandomBarService {
        public randomBar;

        constructor(private $http: ng.IHttpService) {
            this.$http.get(`/api/bars/random`).then((res) => {
                console.log(res.data);
                this.randomBar = res.data;
            });
        }
    }
    angular.module('Barometr').service('RandomBarService', RandomBarService);
}
