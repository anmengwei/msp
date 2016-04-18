//扩展符合项目需求的DataTable
var DataTableExt={
    Table: function(options) {
        var columns = [];
        //取得列名称:
        options.tableObj.find("tr:first th").each(function (i) {
            var data = $(this).attr("data-field-name");
            columns.push({ "data": data});
        });
        //DataTable参数设置
        var dtoption = {
            "bStateSave": true,
            "processing": true,
            "serverSide": true,
            "searching": false,
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "processing": "加载中...",
                "emptyTable": "无数据",
                "info": "显示 _START_ 到 _END_ 共 _TOTAL_ 条记录",
                "infoEmpty": "No records found",
                "infoFiltered": "(filtered1 from _MAX_ total records)",
                "lengthMenu": "每页显示 _MENU_",
                "search": "搜索:",
                "zeroRecords": "No matching records found",
                "paginate": {
                    "previous": "上一页",
                    "next": "下一页",
                    "last": "最后页",
                    "first": "第一页"
                }
            },
            "ajax": {
                "url": options.url,
                "data": function (d) {
                    var start = d["start"];
                    var length = d["length"] <= 0 ? 50 : d["length"];
                    var PageIndex = start % length > 0 ? (start / length + 1) : (start / length);
                    d.PageSize = length;
                    d.PageIndex = PageIndex;
                    delete d.order;
                    delete d.columns;
                    delete d.search;
                    return d;
                },
                "type": "post"
            },
            "columns": columns,
            "columnDefs": [{
                "targets": 0,
                "orderable": false,
                "searchable": false
            }],
            "lengthMenu": [
                [5, 20, 50, 100],
                [5, 20, 50, 100] // change per page values here
            ],
            // set the initial value
            "pageLength": 5,
            "pagingType": "bootstrap_full_number"
        }
        
        return options.tableObj.DataTable(dtoption);
    }
};
