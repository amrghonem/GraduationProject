
landingPage.factory("signupSrv", function ($http) {
    return {
        signUp: function (user)  {
            return $http({
                method: "Post",
                url: "http://localhost:8178/api/Signup",
                headers: {
                    'Content-Type': 'application/json' 
                },
                 data: user,
            });    
        }
    }
})