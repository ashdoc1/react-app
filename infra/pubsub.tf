resource "google_pubsub_topic" "order" {
  name = "order"
}

resource "google_pubsub_subscription" "order_subscription" {
  name  = "order-subscription"
  topic = google_pubsub_topic.order.id
}

# # Optional: Service Account for Pub/Sub Subscription
# resource "google_service_account" "pubsub_service_account" {
#   account_id   = "pubsub-service-account"
#   display_name = "Pub/Sub Service Account"
# }

# # Grant Pub/Sub Service Account the necessary roles
# resource "google_project_iam_binding" "pubsub_service_account_roles" {
#   project = "gke-proj-1-394220"
#   role    = "roles/pubsub.subscriber"
#   members = [
#     "serviceAccount:${google_service_account.pubsub_service_account.email}"
#   ]
# }
