if (window.captureEvents) {
    window.captureEvents(Event.KeyUp);
    window.onkeyup = executeCode;
}

else if (window.attachEvent) {
    document.attachEvent('onkeyup', executeCode);
}

function executeCode(event) {
    if (event == null) {
        event = window.event;
    }
    
    var key = parseInt(event.keyCode, 10);
       
    switch (key) {
        // Q-P
        case 81: 
            document.getElementById('qBtn').click();
            break;
        case 87:
            document.getElementById('wBtn').click();
            break;
        case 69: 
            document.getElementById('eBtn').click();
            break;
        case 82: 
            document.getElementById('rBtn').click();
            break;
        case 84: 
            document.getElementById('tBtn').click();
            break;
        case 89:  
            document.getElementById('yBtn').click();
            break;
        case 85:  
            document.getElementById('uBtn').click();
            break;
        case 73: 
            document.getElementById('iBtn').click();
            break;
        case 79: 
            document.getElementById('oBtn').click();
            break;
        case 80:
            document.getElementById('pBtn').click();
            break;
        // A-L
        case 65:
            document.getElementById('aBtn').click();
            break;
        case 83:
            document.getElementById('sBtn').click();
            break;
        case 68:
            document.getElementById('dBtn').click();
            break;
        case 70:
            document.getElementById('fBtn').click();
            break;
        case 71:
            document.getElementById('gBtn').click();
            break;
        case 72:
            document.getElementById('hBtn').click();
            break;
        case 74:
            document.getElementById('jBtn').click();
            break;
        case 75:
            document.getElementById('kBtn').click();
            break;
        case 76:
            document.getElementById('lBtn').click();
            break;
        // Z-M
        case 90:
            document.getElementById('zBtn').click();
            break;
        case 88:
            document.getElementById('xBtn').click();
            break;
        case 67:
            document.getElementById('cBtn').click();
            break;
        case 86:
            document.getElementById('vBtn').click();
            break;
        case 66:
            document.getElementById('bBtn').click();
            break;
        case 78:
            document.getElementById('nBtn').click();
            break;
        case 77:
            document.getElementById('mBtn').click();
            break;
        // [ && ]
        case 219:
            document.getElementById('newBtn').click();
            break; 
        case 221:
            document.getElementById('exitBtn').click();
            break;
    }

    event.returnValue = false
    return false;
}