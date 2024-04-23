function readFile(fileBrowser) {
    if (navigator.userAgent.indexOf("MSIE") != -1) {
        readFileIE();
    } else if (navigator.userAgent.indexOf("MSIE") != -1) {
        readFileIE(fileBrowser);
    } else if (navigator.userAgent.indexOf("Firefox") != -1 || navigator.userAgent.indexOf("Mozilla") != -1) {
        readFileFirefox(fileBrowser);
    } else {
        alert("Not IE or Firefox (userAgent=" + navigator.userAgent + ")");
    }
}


function readFileIE() {
    alert(document.form1.fileBrowser.value.toString());
}


function readFileFirefox(fileBrowser) {
    try {
        netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
    } catch (e) {
        alert("不能获取，没有权限");
    }
    var fileName = fileBrowser.value;
    alert(fileName);
}