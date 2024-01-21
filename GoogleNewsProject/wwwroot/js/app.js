// Start after the wole DOM is rendereed
$(document).ready(function () {

    // Fetching the loadingSpinner div from the DOM
    const loadingSpinner = $("#loadingSpinner");

    // Display one New - Item
    // get the id of the specific item and sends it in the AJAX call
    // on success- displayes the data that was retrieved in the DOM elements
    const displayPost = (id) => {
        $.ajax({
            type: "GET",
            url: `/GoogleNews/GetNewsItem/${id}`,  // Replace with the actual endpoint for getting a single news item
            data: { id: id },  // Pass the id as a parameter
            dataType: "json",
            success: function (data) {
                // Assuming the server returns the details of the news item in 'data'
                $('#newItemTitle').text(data.title);
                $('#newItemDate').text((data.date));
                $('#newItemBody').html(data.body);
                // adds a Link (<a>) element to the DOM with target="_blank" to open in a new tab
                var newsLink = '<a id="readMoreLink" href="' + data.link + '" target="_blank">Read More -></a>';
                $('#link').empty();
                $('#link').append(newsLink);
            },
            error: function () {
                alert("Error fetching news item details.");
            }
        });
    }
    // gets the news list, and displays it in the newsContainer element, attaching clieck event for each on 
    const displayNews = (news) => {

        var newsContainer = $("#newsContainer");
        // Clear existing content, in order to avoid duplicate content
        newsContainer.empty();

        // Action parallel to Repeater Control
        // Going through the array and for each item - diaplays it in the same pattern
        if (news && news.length > 0) {
            var newsList = '<div id="sidebar" >';
            $.each(news, function (index, item) {
                var newsItemHtml = '<div class="news-item" data-id="' + item.id + '">';
                newsItemHtml += '<a href="#" class="news-link">' + item.title + '</a>';
                newsItemHtml += '</div>';

                // Append the news item to the newsList
                newsList += newsItemHtml;
            });
            newsList += '</div>';
            // Append the newsList item to the newsContainer
            newsContainer.append(newsList);

            // Attach click event using event delegation
            newsContainer.on('click', '.news-link', function () {
                displayPost($(this).closest('.news-item').data('id'));
            });
        } else {
            // Display a message when there are no news items
            newsContainer.html('<p>No news available.</p>');
        }

    }

    // Fetches the news list from AJAX API call
    const getNews = () => {

        // Show loading spinner while fetching data
        loadingSpinner.show();

        // AJAX call to GetNews action
        $.ajax({
            type: "POST",
            url: "/GoogleNews/GetNews",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                // Handle the success, update the newsContainer with the data
                displayNews(data);
            },
            error: function () {
                alert("Error fetching news.");
            },
            complete: function () {
                // Hide loading spinner when the AJAX call is complete (whether success or error)
                loadingSpinner.hide();
            }
        });
    }

    // Fetch news on page load
    getNews();

});

// AJAX Call to clear the HttpCache
// Called when clicking a button
// A corresponding will be displayed according to success / error
const clearCache = () => {
    $.ajax({
        type: "POST",
        url: "/Manage/ClearCache",
        success: function () {
            $('#clearCacheMessage').text("Cache cleared successfully");

        },
        error: function () {
            $('#clearCacheMessage').text("Error clearing cache");

        }
    });
}

// when clicking privacy in the navbar - navigates privicy page. "privacy.html" will be displayed
const privacy = () => {
    location.href = "privacy.html";
}
// when clicking privacy in the navbar - navigates manager page. "manage.html" will be displayed
const manage = () => {
    location.href = "manage.html";
}


