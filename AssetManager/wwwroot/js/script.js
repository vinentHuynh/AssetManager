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
    //let body = document.getElementsByTagName("body")[0];
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


    // percenatge is the value obtained in the setting; 
    // since opactity max= 1;
    brightness.style.opacity = (parseInt(percentage)/100);

    localStorage["brightness"] = percentage;
}

// text weight
function readFontWeight(weight) {
    let font_Weight = document.getElementsByClassName("fontWeight")[0];
    font_Weight.style.fontWeight = parseInt(weight);

    localStorage["fontWeight"] = weight;
}

function readFontStyle(styles) {
    let fontStyle = document.getElementsByClassName("fontStyle")[0];
    fontStyle.style.fontFamily = styles; 

    localStorage["fontStyle"] = styles;
}

function readTheme(theme) {
    let body = document.getElementsByTagName("body")[0];

    if (theme == "dark-mode") body.classList.add(theme);
    else body.classList.remove("dark-mode");

    localStorage["dark-mode"] = theme;
}

// reset budton 
function resetSetting() {
    document.getElementById("app").className = "default";
    localStorage["textSize"] = "default";
    localStorage["fontStyle"] = "default";
    localStorage["fontWeight"] = 300;
    localStorage["brightness"] = 100;
    localStorage["dark-mode"] = "light";
}
