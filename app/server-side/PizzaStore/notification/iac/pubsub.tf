resource "google_pubsub_topic" "notification" {
  name = "notification"
}

resource "google_pubsub_subscription" "notification_subscription" {
  name  = "notification-subscription"
  topic = google_pubsub_topic.notification.id
}