function loadComments(projectId) {
    $.ajax({
        url: '/ProjectManagement/ProjectComment/getComments?projectId=' + projectId,
        method: 'GET',
        contentType: 'application/json',
        success: (data) => {
            console.log(data);
            let commentHtml = '';
            for (let i = 0; i < data.length; i++) {
                commentHtml += '<div class="comment card container">';
                // test for capital or lowercase C in data[i].content
                commentHtml += '<p>' + data[i].content + '</p>';
                commentHtml += '<span>Posted On: ' + new Date(data[i].datePosted).toLocaleDateString() + '</span>';
                commentHtml += '</div>';
            }
            $('#commentsList').html(commentHtml)
        }
    });
}

$(document).ready(() => {
    let projectId = $('#projectComments input[name="ProjectId"]').val();
    loadComments(projectId);

    $('#addCommentForm').submit(e => {
        e.preventDefault();
        let formData = {
            ProjectId: projectId,
            Content: $('#addCommentForm textarea[name="Content"]').val()
        };

        $.ajax({
            url: '/ProjectManagement/ProjectComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: (response) => {
                if (response.success) {
                    $('#addCommentForm textarea[name="Content"]').val('');
                    loadComments(projectId);
                }
                else {
                    alert(response.message);
                }
            },
            error: (xhr, status, error) => {
                alert(`Error ${error}`);
            }
        });
    });
});