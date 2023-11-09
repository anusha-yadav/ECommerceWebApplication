module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        concat: {
            dist: {
                src: ['wwwroot/js/*.js'], // Source files to concatenate
                dest: 'dist/js/bundle.js' // Destination file
            }
        },

        uglify: {
            options: {
                src: 'wwwroot/js/*.js',
                dest: 'dest/js/minified/',
                ext: '.min.js'
            },
            build: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/js/',
                    src: ['*.js'],
                    dest: 'dest/js/minified',
                    ext: '.min.js'
                }]
            }
        },

        cssmin: {
            options: {
                src: 'wwwroot/css/*.css',
                dest: 'dest/css/minified/',
                ext: '.min.css'
            },
            build: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/css/',
                    src: ['*.css'],
                    dest: 'dest/css/minified',
                    ext: '.min.css'
                }]
            }
        },

        watch: {
            scripts: {
                files: ['wwwroot/js/*.js'], // Watch for changes in source files
                tasks: ['concat', 'uglify'], // Execute concatenation and minification on change
                options: {
                    spawn: false,
                },
            },
        },

        mkdir: {
            all: {
                options: {
                    create: ['dest/js/minified', 'dest/css/minified']
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.registerTask('default', ['concat', 'uglify']); // Default task
};