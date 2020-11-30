var photos = [];
var comments = [];
visualizar = () =>
{
    $('#tabla').hide();
    $('#comentarios').hide();   
    let album = $('#album').val();
    if (album != null && album != "")
    {
        GetPhotos(album);
        return;
    }
    
    alert("Debe seleccionar un Album");
}

GetPhotos = (id) =>
{
    $.ajax({
        method: "GET",
        url: "/home/photos/" + id,        
    })
    .done(function (data) {
        photos = JSON.parse(data);
        PrintPhotos();
    })
    .fail(function () {

        alert("Hubo un error en la petición");

    });
}

PrintPhotos = () =>
{
    
    DeleteTable();
    photos.forEach(element => {

        let htmlTags = '<tr class="datos">' +
            '<td>' + element.id + '</td>' +
            '<td><img src="' + element.thumbnailUrl +'" alt="..." class="img-thumbnail"></td>' +
            '<td>' + element.title + '</td>' +
            '<td>' + element.url + '</td>' +
            '<td><a class="btn btn-primary" onclick="SeeComment('+ element.id +')">Ver Comentarios</a></td>' +
            '</tr>';        
        $('#photos tbody').append(htmlTags);
    });

    $('#photos').append('<tr class="datos"><th colspan="3"></th></tr>');
    $('#tabla').show();    
}

DeleteTable = () =>
{

    let rows = $('#photos tr.datos');
    rows.remove();
}

SeeComment = (photo) =>
{
    $('#comentarios').hide();
    $.ajax({
        method: "GET",
        url: "/home/comments/" + photo,
    })
        .done(function (data) {

        comments = JSON.parse(data);
        PrintComments();
    })
    .fail(function () {

        alert("Hubo un error en la petición");
    })
    
}

PrintComments = () => {

    DeleteList();
    comments.forEach(element => {

        let htmlTags = '<li>' +
            '<p><strong>Email: </strong>' + element.email + '</p>' +
            '<p><strong>Name: </strong>' + element.name + '</p>' +
            '<p><strong>Body: </strong>' + element.body + '</p>' +
            '</li >';
        $('#comments').append(htmlTags);
    });

    if (comments.length > 0) {
        $('#comentarios').show();
        $('html, body').animate({
            scrollTop: $("#comentarios").offset().top
        }, 2000);
    } else
    {
        alert("No hay comentario para esta imagen, intenta con otra");
    }

}

DeleteList = () => {

    let rows = $('#comments li');
    rows.remove();
}