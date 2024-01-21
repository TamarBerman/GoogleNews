$(document).ready(function () {
    const loadingSpinner = $("#loadingSpinner");

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
                var newsLink = '<a id="readMoreLink" href="' + data.link + '" target="_blank">Read More -></a>';
                $('#link').empty();
                $('#link').append(newsLink);
            },
            error: function () {
                alert("Error fetching news item details.");
            }
        });
    }

    const displayNews = (news) => {
        var newsContainer = $("#newsContainer");
        
        newsContainer.empty(); // Clear existing content

        if (news && news.length > 0) {
            var newsList = '<div id="sidebar" >';
            $.each(news, function (index, item) {
                var newsItemHtml = '<div class="news-item" data-id="' + item.id + '">';
                newsItemHtml += '<a href="#" class="news-link">' + item.title + '</a>';
                newsItemHtml += '</div>';

                // Append the news item to the container
                newsList += newsItemHtml;
            });
            newsList += '</div>';
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

const privacy = () => {
    location.href = "privacy.html";
}

const manage = () => {
    location.href = "manage.html";
}


