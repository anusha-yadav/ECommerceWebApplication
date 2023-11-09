const gulp = require('gulp');
const uglify = require('gulp-uglify');
const concat = require('gulp-concat');
const rename = require('gulp-rename');
const cleanCSS = require('gulp-clean-css');

gulp.task('default', function () {
    console.log("Welcome to gulp js in Visual Studio");
});

gulp.task('minify-js', function () {
    return gulp.src('wwwroot/js/*.js')
/*        .pipe(concat('all.js'))
*/        .pipe(uglify())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('bundle-js', function () {
    return gulp.src('wwwroot/js/*.js')
        .pipe(concat('bundle.js'))
        .pipe(uglify())
        .pipe(rename('bundle.min.js'))
        .pipe(gulp.dest('wwwroot/js'));
})

gulp.task('bundle-css', function () {
    return gulp.src('wwwroot/css/*.css')
        .pipe(concat('bundle.css'))
        .pipe(cleanCSS())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css'))
})
