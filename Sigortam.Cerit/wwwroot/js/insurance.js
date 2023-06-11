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

    if (validate === 0) {
        var user = {
            Name: document.getElementById("name").value, LastName: document.getElementById("lastName").value, IdentificationNumber: document.getElementById("identificationNumber").value,
            BirthYear: document.getElementById("birthDate").value
        }

        var userIdentityCheckDto = {
            InsuranceStartDate: document.getElementById("insuranceStartDate").value, InsuranceEndDate: document.getElementById("insuranceEndDate").value, PlateNumber: document.getElementById("plateNumber").value,
            User: user, InsuranceCompany: InsuranceCompanyDto, InsuranceId: document.getElementById("insuranceId").value, Price: document.getElementById("price").value
        };

        $.ajax({
            type: "POST",
            url: "/Insurance/AddOrUpdateInsurance/",
            data: userIdentityCheckDto,
            success:  function (result) {
                if (result.code == 101) {
                    alert(result.message);
                    location.href = "/Insurance/Index";
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
