function addOrUpdateInsurance() {
    let $form = $("#form");
    if ($form.length === 0) {
        return null;
    }

    var forms = document.querySelectorAll('.needs-validation-text');
    var validate = 0;
    for (var i = 0; i < forms.length; i++) {
        if (!(forms[i].value)) {
            validate++;
        }
    }
    var insuranceCompany = document.querySelectorAll('.selected')[0];
    var InsuranceCompanyDto = { Name: insuranceCompany.textContent, InsuranceCompanyId: insuranceCompany.getAttribute('value') };
    debugger;
    var VehicleDto = { PlateNumber: document.getElementById("plateNumber").value };
    if (validate === 0) {
        var user = {
            UserId: document.getElementById("userId").value, Name: document.getElementById("name").value, LastName: document.getElementById("lastName").value,
            IdentificationNumber: document.getElementById("identificationNumber").value, BirthYear: document.getElementById("birthDate").value
        }

        var userIdentityCheckDto = {
            InsuranceStartDate: document.getElementById("insuranceStartDate").value, InsuranceEndDate: document.getElementById("insuranceEndDate").value, PlateNumber: document.getElementById("plateNumber").value, PermitNumber: document.getElementById("permitNumber").value,
            User: user,
            InsuranceCompany: InsuranceCompanyDto,
            Vehicle: VehicleDto,
            InsuranceId: document.getElementById("insuranceId").value, Price: document.getElementById("price").value
        };

        $.ajax({
            type: "POST",
            url: "/Insurance/AddOrUpdateInsurance/",
            data: userIdentityCheckDto,
            success:  function (result) {
                if (result.code == 101) {
                    addFilter();
                    alert(result.message);
                    //window.location.href = result.redirectToUrl;
                }
                else {
                    alert(result.message)
                }
            }
        });
    }
    else {
        alert("Bütün alanları doldurduğunuzdan emin olun!")
    }
}
function GetInsuranceInformation(insuranceId) {
    $.ajax({
        type: "POST",
        url: "/Insurance/GetInsuranceInformation/",
        data: { insuranceId: insuranceId },
        dataType: "json",
        success: function (insurance) {
            debugger;
            document.getElementById("userId").value = insurance.insurance.user.userId;
            document.getElementById("insuranceId").value = insuranceId;
            document.getElementById("name").value = insurance.insurance.user.name;
            document.getElementById("lastName").value = insurance.insurance.user.lastName;
            document.getElementById("identificationNumber").value = insurance.insurance.user.identificationNumber;
            document.getElementById("birthDate").value = insurance.insurance.user.birthYear;
            document.getElementById("plateNumber").value = insurance.insurance.vehicle.plateNumber;
            document.getElementById("price").value = insurance.insurance.price;
            document.getElementById("permitNumber").value = insurance.insurance.permitNumber;
            document.getElementById("insuranceStartDate").value = insurance.insurance.insuranceStartDate.replace('T00:00:00', '');
            document.getElementById("insuranceEndDate").value = insurance.insurance.insuranceEndDate.replace('T00:00:00', '');

            let selectedValue = "<div id='selectedInsurancesCompanyText' class='text'><img style='width:50px ; height:30px' src='imgUrl' alt='product'>insuranceCompany</div>";
            selectedValue = selectedValue.replace('imgUrl', insurance.insurance.insuranceCompany.imageSvgUrl).replace('insuranceCompany', insurance.insurance.insuranceCompany.name);
            document.getElementById("selectedInsurancesCompanyText").innerHTML = selectedValue;
            var insuranceCompanyName = document.querySelectorAll('.selectInsurance');
            var insuranceCompanyId = insurance.insurance.insuranceCompany.insuranceCompanyId;

            for (var i = 0; i < insuranceCompanyName.length; i++) {
                insuranceCompanyName[i].classList.remove("active");
                insuranceCompanyName[i].classList.remove("selected");
                if (insuranceCompanyName[i].getAttribute('value') == insuranceCompanyId) {
                    insuranceCompanyName[i].classList.add("active");
                    insuranceCompanyName[i].classList.add("selected");
                }
            }
        }
    });

}
function postBarcode() {
    var form = $('#fileUploadForm')[0];
    var data = new FormData(form);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/Barcode/ImageReader/",
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        success: function (result) {
            debugger;
            document.getElementById('permitNumber').value = result.permitNumber;
            document.getElementById('plateNumber').value = result.plateNumber;
            document.getElementById('identificationNumber').value = result.identificationNumber;
            alert("Gerekli alanlar başarılı bir şekilde dolduruldu.");
        },
        error: function (result) {
            alert("Error");
        }
    });
}
