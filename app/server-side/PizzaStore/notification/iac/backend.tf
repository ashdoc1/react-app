terraform {
  backend "gcs" {
    bucket = "gke-proj-1-394220-tfstate"
    prefix = "notification-service"
  }
}