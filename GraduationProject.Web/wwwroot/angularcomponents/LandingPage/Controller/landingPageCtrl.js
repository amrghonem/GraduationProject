(function () {
    landingPage.controller("landingPageCtrl", function ($scope, signupSrv,$http,loginSrv) {
        $scope.userSignUp = {};
        $scope.userLogin = {};
        $scope.errorsLogin = {};
        $scope.errorsSignUp = {};
        $scope.showSpinnerLogin = false;
        $scope.showSpinnerSign = false;
        $scope.showSuccesspop = false;
        $scope.signUp = function () {
            $scope.showSpinnerSign = true;
            signupSrv.signUp($scope.userSignUp)
                .success(function (data, status) {
                    $scope.showSuccesspop = true;
                    $scope.showSpinnerSign = false;
                    //alert(status);
                    //alert(data.status);
                   
                    if (data.status =="Success")
                    {
                        alert('Seccess');

                        var signBtn = angular.element(document.querySelector('#signPopBtn'));
                        signBtn.attr('data-toggle', 'modal');
                        signBtn.attr('data-target', '#popupSeccess');
                        angular.element(document.querySelector('#signPopBtn')).triggerHandler('click');
                        angular.element(document.querySelector('#close')).click();
                        angular.element(document.querySelector("#signFrm").reset());
                    }
                    if (data.status == "Failed") {
                        $scope.errorsSignUp = data.errors;
                        alert('failed');
                    }
                });
        };

        $scope.login = function () {
            $scope.showSpinnerLogin = true;
            alert("Working");
            loginSrv.login($scope.userLogin)
                .success(function (data, status) {
                    $scope.showSpinnerLogin = false;
                    //alert(status);
                    //alert(data.status);
                    if (data.status ="Success")
                    {
                        alert(data.token);
                        $('#spinner').addClass('hide');
                        $(this).attr('disabled', false);
                    }
                    if (data.status = "Failed") {
                        $scope.errorsLogin = data.errors;
                    }
                });
        };

    });
})();