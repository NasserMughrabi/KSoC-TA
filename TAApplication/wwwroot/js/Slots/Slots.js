/**
  Author:    Nasser Mughrabi
File Contents:
    This class is js file to handle user interaction with the availability web page
 */

let bg_color = 'black';
let rect_color = 0x96187b;
let width = 900;
let height = 590;
var changed = [];
var openTimes = [];
var color = 0xffffff;
var mouse_down = false;
var mouse_dragging = false;


setup();
function setup() {
    console.log('setup');

    app = new PIXI.Application({ backgroundColor: bg_color });
    app.renderer.resize(width, height);
    $("#canvas_div").append(app.view);

    getSlotsAndDraw();
    addTextsOnGraphic();
}

function getSlotsAndDraw() {
    $.get("/Slots/GetSchedule")
        .done(function (data) {
            // get the slots from DB
            openTimes = [];
            for (var item in data.message) {
                if (data.message[item].isOpen) {
                    openTimes.push(data.message[item].dayAndTime);
                }
            }

            // Build a square and line for each possible slot
            for (let j = 0; j < 5; j++) {
                hour = 8
                time = 0
                daytime = "am"
                for (let i = 0; i < 48; i++) {
                    if (i > 14) {
                        daytime = "pm"
                    }

                    time = time + 15
                    if (time == 60) {
                        time = 0
                        hour = hour + 1
                    }
                    if (time > 60) {
                        time = 0
                        time = time + 15
                        hour = hour + 1
                    }
                    timeString = hour + ":" + time + daytime;

                    // use database to pass open/closed color
                    build_square(i, j, timeString);
                }
            }
            for (let i = 0; i < 48 / 4 + 1; i++) {
                draw_lines(i)
            }
        });
}

function addTextsOnGraphic() {
    for (let j = 0; j < 5; j++) {
        days_titles(j, j)
    }

    for (let i = 0; i < 48 / 4 + 1; i++) {
        time = 8 + i
        daytime = "am";
        if (i > 3) {
            daytime = "pm";
        }
        if (i > 4) {
            j = i - 5;
            time = 1 + j;
        }
        draw_lines(i)
        draw_times(i, time, daytime)
    }

    draw_guids(1)
}

function draw_guids(id1) {
    var square1 = new PIXI.Graphics();
    square1.beginFill(rect_color);
    square1.drawRect(0, 0, 100, 10);
    square1.x = 300;
    square1.y = 51 + 500 * id1;
    app.stage.addChild(square1);

    const style = new PIXI.TextStyle({
        fill: "white",
        fontSize: 20
    });
    var text1 = new PIXI.Text("Not Available", style);
    text1.x = 295;
    text1.y = 53 + 510 * id1;
    app.stage.addChild(text1);

    var square2 = new PIXI.Graphics();
    square2.beginFill(0x3602f);
    square2.drawRect(0, 0, 100, 10);
    square2.x = 480;
    square2.y = 51 + 500 * id1;
    app.stage.addChild(square2);

    var text2 = new PIXI.Text("Available", style);
    text2.x = 490;
    text2.y = 53 + 510 * id1;
    app.stage.addChild(text2);

    const style2 = new PIXI.TextStyle({
        fill: "white",
        fontSize: 12
    });
    var text3 = new PIXI.Text("Click and drag to set/un-set available times", style2);
    text3.x = 50;
    text3.y = 51 + 500 * id1;
    app.stage.addChild(text3);

}

function draw_times(i, time, daytime) {
    const style = new PIXI.TextStyle({
        fill: "white",
        fontSize: 20
    });
    var text = new PIXI.Text(time + ":00 " + daytime, style);
    text.x = 800;
    text.y = 40 + 40 * i;
    app.stage.addChild(text);
}

function draw_lines(i) {
    var square = new PIXI.Graphics();
    square.beginFill(0xffffff);
    square.drawRect(0, 0, 750, 1);
    square.x = 20;
    square.y = 51 + 40 * i;
    app.stage.addChild(square);
}

function days_titles(id, col) {

    var day = "";
    if (col == 0) {
        day = "Monday";
    } else if (col == 1) {
        day = "Tuesday";
    } else if (col == 2) {
        day = "Wendesday";
    } else if (col == 3) {
        day = "Thursday";
    } else if (col == 4) {
        day = "Friday";
    }

    const style = new PIXI.TextStyle({
        fill: "white",
        fontSize: 20
    });
    var text = new PIXI.Text(day, style);
    text.x = 50 + 150 * col;
    text.y = 20;
    text.font = "10px Arial";
    app.stage.addChild(text);
}

function build_square(time, day, timeString) {
    var square = new PIXI.Graphics();

    square.beginFill(rect_color);
    square.drawRect(0, 0, 100, 10);
    square.x = 50 + 150 * day;
    square.y = 51 + 10 * time;
    square.interactive = true;

    if (day == 0) {
        day = "Monday";
    } else if (day == 1) {
        day = "Tuesday";
    } else if (day == 2) {
        day = "Wendesday";
    } else if (day == 3) {
        day = "Thursday";
    } else if (day == 4) {
        day = "Friday";
    }
    square.id = day + " " + timeString;

    // change color if square dayandtime is open
    for (var item in openTimes) {
        if (openTimes[item] == day + " " + timeString) {
            square.beginFill(0x3602f);
            square.drawRect(0, 0, 100, 10);
        }
    }
    app.stage.addChild(square);

    square.on('mousedown', pointer_down);
    square.on('mouseup', pointer_up);
    return square;
}

function pointer_down() {
    // if the sqaure slot is open, then change its color from green to darkred, otherwise, change it to green

    // change in the database
    if (changed.includes(this.id)) {
        // unchange
        var index = changed.indexOf(this.id);
        changed.splice(index, 1);
    } else {
        changed.push(this.id);
    }

    // change the color
    if (openTimes.includes(this.id)) {
        // the color is green now
        this.clear();
        color = rect_color;
        this.beginFill(color);
        this.drawRect(0, 0, 100, 10);
        var index = openTimes.indexOf(this.id);
        openTimes.splice(index, 1);
    } else {
        this.clear();
        color = 0x3602f;
        this.beginFill(color);
        this.drawRect(0, 0, 100, 10);
        openTimes.push(this.id);
    }
}

function pointer_up() {
    mouse_down = false;
    mouse_dragging = false;

    if (changed.length > 0) {
        $(".warning").css({ display: 'block' });
    } else {
        $(".warning").css({ display: 'none' });
    }
}

function pointer_over() {
    console.log(`I am square ${this.id}`);
    if (this.id == 2 && mouse_down) {
        this.clear();
        this.beginFill(color);
        this.drawRect(0, 0, 100, 100);
    }
}

function save(id) {
    console.log(changed)
    $(".loader").css({ display: 'block' });
    $.post({
        url: "/Slots/SetSchedule",
        data: {
            id: id,
            times: changed
        }
    })
        .done(function (data) {
            console.log("Sent slots\n", data);
            $(".loader").css({ display: 'none' });
            $(".warning").css({ display: 'none' });
        });
}

function onDragMove() {
    console.log('Mouse moving1');
    if (mouse_dragging) {
        console.log('Mouse moving');
    }
}
