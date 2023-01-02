const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});


$(function (e) {

	$('input[type=radio][name=UserType]').change((e) => {
		let workerType = $(e.target).val();
		if (workerType == 1) {
			$("#chiefSelection").attr("class", "");
		} else {
			$("#chiefSelection").attr("class", "dontshow");
			$("#chiefSelection").val('');
		}
	});

    $("#createaccountbtn").click((e) => {

        let userInfo = {
            email: $("#registerEmail").val(),
            username: $("#registerUsername").val(),
            password: $("#registerPassword").val(),
            firstName: $("#registerFirstName").val(),
            lastName: $("#registerLastName").val(),
            fatherName: $("#registerFatherName").val(),
            position: $("#registerPosition").val(),
            chiefId: $("#chiefSelection").val(),
            userType: $("input[type=radio][name=UserType]:checked").val()
        };


        $.ajax({
            method: "POST",
            url: "/auth/register",
            data: { registerDto: userInfo }
        }).done((data) => {

            if (!data.success) {
                alert(data.errorDescription);
            }
            else {
                console.log(data.redirectToUrl);
                reload("home", "index");
            }

        }).fail((data) => {
            console.log("FAILED")
            console.log(data);
        });
    });


    $("#loginbtn").click((e) => {

        let userLoginInfo = {
            username: $("#loginUsername").val(),
            password: $("#loginPassword").val()
        };


        $.ajax({
            method: "POST",
            url: "/auth/login",
            data: { loginDto: userLoginInfo }
        }).done((data) => {

            if (!data.success) {
                alert(data.errorDescription);
            } else {
                reload("home", "index");
            }
            
        }).fail((data) => {
            console.log("FAILED")
            console.log(data);
        });
    });
});