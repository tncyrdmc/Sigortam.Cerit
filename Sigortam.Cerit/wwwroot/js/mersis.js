function identityCheck() {
        debugger;
        let $form = $("#form");
        if ($form.length === 0) {
            return null;
        }
        var data = {
            Name: document.getElementById("name").value, LastName: document.getElementById("lastName").value, IdentificationNumber: document.getElementById("identificationNumber").value, BirthDate: document.getElementById("birthDate").value,
            PlateNumber: document.getElementById("plateNumber").value
        };

        $.post("Mersis/IdentityCheck", { userPostDto: data}, function (result) {
            debugger;
            if (result.code == 101) {
                location.href = "/Home/Index";
            }
            else {
                location.href = "/UserLogin/Index";
            }
            //if (result == "Succeeded") {
            //    $(".card-alert .close").click(function () {
            //        $(this).closest(".card-alert").fadeOut("slow");
            //    });
            //}
        });
    }
