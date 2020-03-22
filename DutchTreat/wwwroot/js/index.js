$(document).ready( function () {
    var theForm = $("#theForm");
    theForm.hide();

    const buyButton = $("#buyButton");

    buyButton.on("click", function () {
        console.log("buying Item");
    });


    //const productInfo = document.getElementsByClassName("product-props");
    //const listItems = productInfo.item[0].children;
    //console.log(listItems);


    const productList = $(".product-props li");
    console.log(productList);
    productList.on("click", function () {
        console.log("you clicked on " + $(this).text())
    });


    // manipulate form with jquery 

    const loginForm = $("#loginToggle");
    console.log(loginForm);
    const popupForm = $(".popup-form");
    console.log(popupForm);
    loginForm.on("click", function () {
        popupForm.fadeToggle(1000);
    })

});