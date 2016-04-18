//扩展符合项目需求的DataTable
var DataTableExt={
    Table: function(options) {
        var columns = [];
        //取得列名称:
        options.tableObj.find("tr:first th").each(function (i) {
            var data = $(this).attr("data-field-name");
            columns.push({ "data": data});
        });
        //处理参数
        var getParams = function (d) {
            var start = d["start"];
            var length = d["length"] <= 0 ? 50 : d["length"];
            var PageIndex = start % length > 0 ? (start / length + 1) : (start / length);
            d.PageSize = length;
            d.PageIndex = PageIndex;
            delete d.order;
            delete d.columns;
            delete d.search;
            return d;
        };

        //语言
        var language = options.language || {
            "info": "显示 _START_ 到 _END_ 共 _TOTAL_ 记录",
            "infoEmpty": "无数据",
            "infoFiltered": "(filtered1 from _MAX_ total records)",
            "lengthMenu": "每页显示 _MENU_",
            "search": "搜索:",
            "zeroRecords": "未查询到数据",
            "paginate": {
                "next": "下一页",
                "previous": "上一页",
                "first": "首页",
                "last": "末页"
            }
        };

        //DataTable参数设置
        var dtOptions = {
            "bStateSave": true,
            "processing": true,
            "serverSide": true,
            "deferRender": true,
            "processing": true,
            "ajax": {
                "url": options.searchUrl,
                "data": function (d) { return getParams(d); },
                "type": "post"
            },
            "sScrollY": "auto",
            "language": language,
            "bScrollCollapse": false,
            "columns": columns,
            "createdRow": function (nRow, aData, iDataIndex) {
                var id = aData[options.primaryId];
                $(nRow).find(".bind-data-id").val(id).attr("data-id", id);
                if (options.fnCreateRow) {
                    options.fnCreateRow(nRow, aData, iDataIndex);
                }
            }
        };
        return options.tableObj.DataTable(dtOptions);
    }
};
