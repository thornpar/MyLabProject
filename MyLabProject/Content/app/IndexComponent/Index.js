angular.module('MyLabProject').directive('index', function () {
    return {
        templateUrl: 'Content/app/IndexComponent/Index.html',
        controller: 'MyLabProject.indexController',
    };
});