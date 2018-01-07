////////////////////////////////////////////////////////////////////////
//key press enter from search box should auto trigger btnGo
$(document).on("keypress", "#txtSearchQuery", function (e) {
    if (e.keyCode === 13 || e.which === "13") {
        e.preventDefault();
        $("#btnGo").trigger("click");
    }
});
//////////////////////////////////////////////////////////////////////
//Method to do an ajax call to search the movie with the entered query
function getSearchResults() {
    $("#divLoading").show();
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Home/SearchMovie",
        data: '{searchText: "' + $("#txtSearchQuery").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response !== null) {
                //render the html response to the search result place holder div
                $("#divSearchResult").html(response);
            } else {
                $("#divSearchResult").html("<span class='error'>An error occurred while processing your request.Please try again later.</span>");
            }
            $("#divLoading").hide();
        },
        failure: function (response) {
            alert(response);
            $("#divLoading").hide();
        }
    });
}
/////////////////////////////////////////////////////////////////////////