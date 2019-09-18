var employeeController = {
    init: function () {
        employeeController.loadData();
        employeeController.registerEvent();
    },
    // 
    registerEvent: function () {
        $('.txtAge').keypress(function (e) {
            if (e.which == 13) {
                var id = $(this).data('id');
                var value = $(this).val();
                employeeController.updateData(id, value);
            }
        })
    },
    updateData: function (id, value) {
        var data = {
            ID: id,
            Age: value
        };
        $.ajax({
            url: 'Update',
            type: 'POST',
            dataType: 'JSON',
            //truyền lên bằng string
            data: { model: JSON.stringify(data) },
            success: function (response) {
                if (response.status == true) {
                    alert("Update Thành Công");
                } else {
                    alert("Update Không Thành Công");
                }
            }
        })
    },
    loadData: function () {
        $.ajax({
            url: 'LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var temp = $('#dataTemp').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(temp, {
                            ID: item.ID,
                            Name: item.Name,
                            Age: item.Age,
                            Status: item.Status == true ? "<button type=\"button\" class=\"btn btn-success\">Active</button>" : "<button type=\"button\" class=\"btn btn-warning\">Locked</button>"
                        });
                    })
                    $('#tblData').html(html);
                    employeeController.registerEvent();
                }
            }

        });
    }
}
employeeController.init();