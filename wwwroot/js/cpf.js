function valCPF(e,campo){
 var tecla=(window.event)?event.keyCode:e.which;
 if((tecla > 47 && tecla < 58)){
 mascara(campo, '###.###.###-##');
 return true;
 }
 else{
 if (tecla != 8) return false;
 else return true;
 }
}

function valCEP(e, campo) {
    var tecla = (window.event) ? event.keyCode : e.which;
    if ((tecla > 47 && tecla < 58)) {
        mascara(campo, '#####-###');
        return true;
    }
    else {
        if (tecla != 8) return false;
        else return true;
    }
}

function valCNPJ(e, campo) {
    var tecla = (window.event) ? event.keyCode : e.which;
    if ((tecla > 47 && tecla < 58)) {
        mascara(campo, '##.###.###/####-##');
        return true;
    }
    else {
        if (tecla != 8) return false;
        else return true;
    }
}
