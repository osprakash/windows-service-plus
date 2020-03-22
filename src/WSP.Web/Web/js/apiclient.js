function getJobStats(callback) {
    $.get('api/stats', function (data) {
        var d = JSON.parse(data);
        callback(d);
    });
}

function getJob(id) {

}

function addJob(id) {

}