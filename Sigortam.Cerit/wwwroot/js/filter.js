function addFilter() {
    var selectedInsuranceCompanyId = 0;
    if (document.querySelectorAll('.selected')[0] !== undefined) {
        selectedInsuranceCompanyId = document.querySelectorAll('.selected')[0].getAttribute('value');
    }
    var status = document.getElementById("statusFilter");
    var selectedStatus = status.options[status.selectedIndex].value;
    debugger;
    let searchText = document.getElementById('search-bar').value;

    var year = document.getElementById("yearFilter");
    var selectedYear = year.options[year.selectedIndex].value;

    var month = document.getElementById("monthFilter");
    var selectedMonth = month.options[month.selectedIndex].value;

    var filterDto = { StatusType: selectedStatus, InsuranceCompanyId: selectedInsuranceCompanyId, SearchText: searchText, Year: selectedYear, Month: selectedMonth, };


    $.ajax({
        type: "POST",
        url: "/Insurance/FilterInsurance/",
        data: filterDto,
        success: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
}
function getExpiredInsurances() {
    $.ajax({
        type: "POST",
        url: "/Insurance/GetExpiredInsurances/",
        success: function (response) {
            alert("Süresi yaklaşan kayıtlar başarılı bir şekilde getirildi.")
            window.location.href = response.redirectToUrl;
        }
    });
}
function filtertSearch() {
    let searchText = document.getElementById('search-bar').value;
    debugger;
    var filterDto = { SearchText: searchText };
        $.ajax({
            type: "POST",
            url: "/Insurance/FilterInsurance/",
            data: filterDto,
            success: function (response) {
                window.location.href = response.redirectToUrl;
            }
        });
}
function removeCash() {
    $.ajax({
        type: "POST",
        url: "/Insurance/RemoveCash/",
        success: function (response) {
            alert("Filtreler başarılı bir şekilde temizlendi")
            window.location.href = response.redirectToUrl;
        }
    });
}
function filterSort(filterSortingId) {
    debugger;
    var filterData = { FilterSort: filterSortingId, SearchWord: "" };
    $.ajax({
        type: "POST",
        url: "/Insurance/FilterSorting/",
        data: filterData,
        //dataType: "json",
        success: async function (response) {
            //debugger;
            //var result = response.sortDescription ;
            //if (result.includes('Desc')) {
            //    document.getElementById(result.replace('Desc', '')).classList.remove(result);
            //    document.getElementById(result.replace('Desc', '')).classList.add(result.replace('Desc', ''));
            //}
            //else {
            //    document.getElementById(result).classList.remove(result);
            //    document.getElementById(result).classList.add(result + "Desc");
            //}
            window.location.href = response.redirectToUrl;
        }
    });
}

