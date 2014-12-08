

function dayClicked(event) {
    var v = $(event.target).find("var.dayVar").html();
    
    alert("Day Clicked:" + v);
}