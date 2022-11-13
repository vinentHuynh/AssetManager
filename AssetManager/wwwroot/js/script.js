function setTheme(themeName) {
    // add a new css link

    let newLink = document.createElement("link");
    newLink.setAttribute("id", "theme");
    newLink.setAttribute("rel", "Stylesheet");
    newLink.setAttribute("type", "text/css");
    newLink.setAttribute("href", `css/app-${themeName}.css`);


    // remove the them link the tag
    let head = document.getElementsByTagName("head")[0];
    head.querySelector("#theme").remove();
    heead.appendChild(newLink);
}

// Text size
function textSize(val) {
   // let body = document.getElementsByTagName("body")[0];
    //body.className = val;
    let app = document.getElementById("app"); 
   
    app.className = val;

    localStorage["textSize"] = val;
}

// readonly local storage 
function readLocalStorage(key) {
    return localStorage[key];
}


// brightness function 
function readBrightness(percentage) {

    let brightness = document.getElementsByClassName("brightness")[0];

    percentage /= 100;
    // percenatge is the value obtained in the setting; 
    // since opactity max= 1;
    brightness.style.opacity = percentage;

}

function readFontWeight(weight) {
    let fontWeight = document.getElementsByClassName("fontWeight")[0];
    fontWeight.style.fontWeight = weight;

}

function readTextStyle(styles) {
    let fontStyle = document.getElementsByClassName("fontStyle")[0];
    fontStyle.style.fontFamily = styles; 
}

// reset budton 
function resetSetting() {
    document.getElementById("app").className = "default";
    localStorage["textSize"] = "default";
}