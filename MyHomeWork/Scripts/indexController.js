var Url = {
    HomeUrl: "/Home/PageList/",
    DelUrl: "/Home/Delete/"
}
var app = angular.module('myApp', ['ngRoute'], function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: '/page.html'
    });
})
.controller("indexController", ['$http', function ($http) {
    var self = this;
    self.order = '-';
    self.orderType = 'Categoryyy';
    self.ChangeOrder = function (type) {
        self.orderType = type;
        if (self.order === '') {
            self.order = '-';
        } else {
            self.order = '';
        }
    }
    self.DelData = function (id, pageIndex) {
        if (id === null) return alert("請正常瀏覽網頁");
        $http.post(Url.DelUrl + id, { pageIndex: pageIndex })
            .success(function (jsonObj) {
                self.GetList(jsonObj.pageIndex)
            });
    },
    self.pageIndex = 1;
    self.list = [];
    self.pageInfo = [];
    self.GetList = function (pageIndex) {
        if (pageIndex < 1) pageIndex = 1;
        if (pageIndex > self.pageCount) pageIndex = self.pageCount;
        $http.post(Url.HomeUrl, { pageIndex: pageIndex })
            .success(function (jsonObj) {
                self.list = jsonObj.Models;
                self.pageIndex = jsonObj.PageIndex;
                self.pageInfo = new PageList(jsonObj.PageIndex, jsonObj.PrevIndex, jsonObj.NextIndex, jsonObj.PageSize, jsonObj.PageBarSize, jsonObj.TotalCount);
            });
    }
    self.GetList(1);
}]);
app.filter('myCategoryyyFormat', function () {
    return function (c) {
        switch (c) {
            case 0: return "收入";
            case 1: return "支出";
        }
        return "";
    };
});

function PageList(pageIndex, prevIndex, nextIndex, pageSize, pageBarSize, totalCount) {
    this.pageIndex = pageIndex;
    this.prevIndex = prevIndex;
    this.nextIndex = nextIndex;
    this.pageSize = pageSize;
    this.pageBarSize = pageBarSize;
    this.totalCount = totalCount;
    var skipCount = (pageIndex + pageBarSize) > totalCount ? totalCount + 1 : (pageIndex + pageBarSize);
    var arr = [];
    for (var i = pageIndex; i < skipCount; i++) {
        arr.push(i);
    }
    this.pageRowArr = arr;
}