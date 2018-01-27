require([
        'adminLTE',
        'jquery',
        'jquery-ui-core',
        'bootstrap',
        'bforms-ajax'
], function () {

    var AdminLTE = function () {

    };
    // fix slimscroll
    AdminLTE.prototype._fixSidebar = function () {

        //Make sure the body tag has the .fixed class
        if (!$("body").hasClass("fixed")) {
            return;
        }

        //Add slimscroll
        $(".sidebar").slimscroll({
            height: ($(window).height() - $(".header").height()) + "px",
            color: "rgba(0,0,0,0.2)"
        });


    };
    // fix css - check height
    AdminLTE.prototype._fix = function () {

        //Get window height and the wrapper height
        var height = $(window).height() - $("body > .header").height();
        $(".wrapper").css("min-height", height + "px");
        var content = $(".wrapper").height();
        //If the wrapper height is greater than the window
        if (content > height)
            //then set sidebar height to the wrapper
            $(".left-side, html, body").css("min-height", content + "px");
        else {
            //Otherwise, set the sidebar to the height of the window
            $(".left-side, html, body").css("min-height", height + "px");
        }

    };
    // init sidebar & toolbar components
    AdminLTE.prototype._initComponents = function () {

        var currentContext = this;
        
        //Enable sidebar toggle
        $("[data-toggle='offcanvas']").click(function (e) {
            e.preventDefault();

            var active = true;
            
            //If window is small enough, enable sidebar push menu
            if ($(window).width() <= 992) {
                
            } else {                
                active = false;
            }

            var data = {
                active: active
            };
            
            $.bforms.ajax({
                name: 'toggleSidebar',
                url: requireConfig.websiteOptions.toggleSidebarUrl,
                callbackData: data,
                success: $.proxy(currentContext._ajaxToggleSidebarSuccess),
                error: $.proxy(currentContext._ajaxToggleSidebarError),
            });

        });

        //Add hover support for touch devices
        $('.btn').bind('touchstart', function () {
            $(this).addClass('hover');
        }).bind('touchend', function () {
            $(this).removeClass('hover');
        });

        //Activate tooltips
        $("[data-toggle='tooltip']").tooltip();

        /*     
         * Add collapse and remove events to boxes
         */
        $("[data-widget='collapse']").click(function () {
            //Find the box parent        
            var box = $(this).parents(".box").first();
            //Find the body and the footer
            var bf = box.find(".box-body, .box-footer");
            if (!box.hasClass("collapsed-box")) {
                box.addClass("collapsed-box");
                bf.slideUp();
            } else {
                box.removeClass("collapsed-box");
                bf.slideDown();
            }
        });

        /*
         * ADD SLIMSCROLL TO THE TOP NAV DROPDOWNS
         * ---------------------------------------
         */
        $(".navbar .menu").slimscroll({
            height: "200px",
            alwaysVisible: false,
            size: "3px"
        }).css("width", "100%");

        /*
         * INITIALIZE BUTTON TOGGLE
         * ------------------------
         */
        $('.btn-group[data-toggle="btn-toggle"]').each(function () {
            var group = $(this);
            $(this).find(".btn").click(function (e) {
                group.find(".btn.active").removeClass("active");
                $(this).addClass("active");
                e.preventDefault();
            });

        });

        $("[data-widget='remove']").click(function () {
            //Find the box parent        
            var box = $(this).parents(".box").first();
            box.slideUp();
        });

        /* Sidebar tree view */
        $(".sidebar .treeview").tree();

        /* 
         * Make sure that the sidebar is streched full height
         * ---------------------------------------------
         * We are gonna assign a min-height value every time the
         * wrapper gets resized and upon page load. We will use
         * Ben Alman's method for detecting the resize event.
         * 
         **/

        //Fire upon load
        this._fix();

        var context = this;
        //Fire when wrapper is resized
        $(".wrapper").resize(function () {
            context._fix();
            context._fixSidebar();
        });

        //Fix the fixed layout sidebar scroll bug
        this._fixSidebar();

        /*
               * We are gonna initialize all checkbox and radio inputs to 
               * iCheck plugin in.
               * You can find the documentation at http://fronteed.com/iCheck/
           
        $("input[type='checkbox'], input[type='radio']").iCheck({
            checkboxClass: 'icheckbox_minimal',
            radioClass: 'iradio_minimal'
        });
            */
    };
    // sidebar toggle event success
    AdminLTE.prototype._ajaxToggleSidebarSuccess = function (response, callbackData) {

        if (callbackData.active != null && callbackData.active) {
            
            $('.row-offcanvas').toggleClass('active');
            $('.left-side').removeClass('collapse-left');
            $('.right-side').removeClass('strech');
            $('.row-offcanvas').toggleClass('relative');

        } else {
            
            if (response != null &&
                response.SidebarOpen != null && !response.SidebarOpen) {
                
                if (!$('.right-side').hasClass('strech')) {
                    $('.right-side').addClass('strech');

                }
                
                if (!$('.right-side').hasClass('collapse-left')) {
                    $('.left-side').addClass('collapse-left');

                }
            }
            else if (response != null &&
                response.SidebarOpen != null && response.SidebarOpen) {

                $('.right-side').removeClass('strech');
                $('.left-side').removeClass('collapse-left');
            }
            
        }

    };
    // sidebar toggle event fail
    AdminLTE.prototype._ajaxToggleSidebarError = function(response, callbackData) {

        console.trace();
    };
    // init function
    AdminLTE.prototype._init = function () {

        this._initComponents();

    };
    // init current module when document is ready
    $(document).ready(function () {
        var ctrl = new AdminLTE();
        ctrl._init();
    });

});


