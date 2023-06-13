function addFilter() {
    var selectedInsuranceCompanyId = 0;
    if (document.querySelectorAll('.selected')[0] !== undefined) {
        selectedInsuranceCompanyId = document.querySelectorAll('.selected')[0].getAttribute('value');
    }
    var status = document.getElementById("statusFilter");
    var statusValue = status.options[status.selectedIndex].value;
    var filterDto = { StatusType: statusValue, InsuranceCompanyId: selectedInsuranceCompanyId };


    $.ajax({
        type: "POST",
        url: "/Insurance/FilterInsurance/",
        data: filterDto,
        success: function (response) {
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
function resetFilter() {
    var filterDto = { IsResetCasheFilter: true };
    document.getElementById('resetFilter').value = "";
    $.ajax({
        type: "POST",
        url: "/Insurance/FilterInsurance/",
        data: filterDto,
        success: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
}
function filterSort(filterSortingId) {
    var filterData = { FilterSort: document.getElementById(filterSortingId).classList[0], SearchWord: "" };
    $.ajax({
        type: "POST",
        url: "/Insurance/FilterSorting/",
        data: filterData,
        //dataType: "json",
        success: async function (result) {
            if (result.includes('Desc')) {
                document.getElementById(result.replace('Desc', '')).classList.remove(result);
                document.getElementById(result.replace('Desc', '')).classList.add(result.replace('Desc', ''));
            }
            else {
                document.getElementById(result).classList.remove(result);
                document.getElementById(result).classList.add(result + "Desc");
            }
        }
    });
}

