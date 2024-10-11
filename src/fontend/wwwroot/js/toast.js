var Toast = (function () {
    function ShowToastWarning(content) {
        document.getElementById("solidwarningToastContent").textContent = content;
        document.getElementById("solidwarningToastBtn").click();
    }
    function ShowToastInfo(content) {
        document.getElementById("solidinfoToastContent").textContent = content;
        document.getElementById("solidinfoToastBtn").click();
    }
    function ShowToastSuccess(content) {
        document.getElementById("solidsuccessToastContent").textContent = content;
        document.getElementById("solidsuccessToastBtn").click();
    }
    function ShowToastError(content) {
        document.getElementById("soliddangerToastContent").textContent = content;
        document.getElementById("soliddangerToastBtn").click();
    }

    return { ShowToastWarning, ShowToastInfo, ShowToastSuccess, ShowToastError };
})