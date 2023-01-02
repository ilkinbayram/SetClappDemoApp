function cleanWorkerRequestInputs() {

    var inputs = $(".modal input");
    var textareas = $(".modal textarea");
    var selectboxes = $(".modal select");

    for (let i = 0; i < inputs.length; i++) {
        if (!$(inputs[i]).hasClass("user-holder"))
            $(inputs[i]).val('');
    }

    for (let i = 0; i < textareas.length; i++) {
        $(textareas[i]).val('');
    }
    for (let i = 0; i < inputs.length; i++) {
        $(selectboxes[i]).val('1');
    }

}


$("#closeCreateRequestModal").click(() => {
    cleanWorkerRequestInputs();
});


$("#createCreateRequestModal").click((e) => {

    console.log("clicked");

    let createRequestModel = {
        startFrom: $("#datepickerStartFrom").val(),
        finishDate: $("#datepickerFinish").val(),
        replacerUserId: $("#replacerRequestId").val(),
        requestType: $("#requestTypeIdentifier").val(),
        additionalDescription: $("#additionalNoteArea").val()
    }

    $.ajax({
        method: "POST",
        url: "/home/index",
        data: { workerRequestDto: createRequestModel }
    }).done((data) => {
        if (!data.success) {
            alert(data.errorDescription);
            return;
        }
        else {
            reload("home", "index");
        }
    }).fail((data) => {
        console.log("FAILED")
        console.log(data);
    });
});

$(".pdf-print").click((e) => {
    let requestId = $(e.target).data("reqid");
    window.open(`../request/printrequest/${requestId}`, "_blank");
});


$(".details-show-button").click((e) => {
    let requestIdNumber = $(e.target).data("requestid");

    $.ajax({
        method: "POST",
        url: "/request/getrequestdetails",
        data: { requestId: requestIdNumber }
    }).done((data) => {
        $("#requestStatusDetail").text(data.requestType)
        $("#requestNumber").text(data.documentNumber)
        $("#requestOwnerFirstName").text(data.firstName)
        $("#requestOwnerLastName").text(data.lastName)
        $("#requestOwnerFatherName").text(data.fatherName)
        $("#requestOwnerPosition").text(data.position)
        $("#requestReplacerFirstName").text(data.replacerFirstName)
        $("#requestReplacerLastName").text(data.replacerLastName)
        $("#requestReplacerFatherName").text(data.replacerFatherName)
        $("#requestStartDate").text(data.startFrom)
        $("#requestFinishDate").text(data.finishDate)
        $("#requestAdditionalNote").text(data.additionalDescription)

        $(".actionbtn").attr("data-requestid", data.requestId)
        console.log(data.requestId);

        if (data.requestStatus !== data.userType) {
            $(".actionbtn").attr("disabled", "true");
        } else {
            console.log("REMOVE disable")
            $('.actionbtn').prop("disabled", false)
        }
    }).fail((data) => {
        console.log("FAILED")
        console.log(data);
    });
});


$(".actionbtn").click((e) => {

    let actionCommand = $(e.target).data("actioncommand");
    let requestIdNumber = $(e.target).data("requestid");

    $.ajax({
        method: "POST",
        url: "/request/" + actionCommand,
        data: { requestId: requestIdNumber }
    }).done((data) => {

        $("#requestStatusDetail").text(data.requestType)
        $("#requestNumber").text(data.documentNumber)
        $("#requestOwnerFirstName").text(data.firstName)
        $("#requestOwnerLastName").text(data.lastName)
        $("#requestOwnerFatherName").text(data.fatherName)
        $("#requestOwnerPosition").text(data.position)
        $("#requestReplacerFirstName").text(data.replacerFirstName)
        $("#requestReplacerLastName").text(data.replacerLastName)
        $("#requestReplacerFatherName").text(data.replacerFatherName)
        $("#requestStartDate").text(data.startFrom)
        $("#requestFinishDate").text(data.finishDate)
        $("#requestAdditionalNote").text(data.additionalDescription)

        if (data.requestStatus !== data.userType) {
            console.log("add disable")
            $(".actionbtn").attr("disabled", "true");
        } else {
            console.log("REMOVE disable")
            $(".actionbtn").attr("data-requestid", requestIdNumber)
            $('.actionbtn').prop("disabled", false)
        }

        reload("home", "index");

    }).fail((data) => {
        console.log("FAILED")
        console.log(data);
    });
});