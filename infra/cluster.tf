
# # Create the service account for GKE Workload Identity
# resource "google_service_account" "gke_workload_identity_sa" {
#   account_id   = "gke-workload-identity-sa"
#   display_name = "GKE Workload Identity Service Account"
# }

# # IAM policy binding to allow GKE nodes to impersonate the service account
# resource "google_service_account_iam_binding" "workload_identity_user" {
#   service_account_id = google_service_account.gke_workload_identity_sa.id
#   role               = "roles/iam.workloadIdentityUser"
#   members            = [
#     "serviceAccount:${var.project_id}.svc.id.goog[default/default]"
#   ]
# }

# resource "google_container_cluster" "primary" {
#   name               = var.cluster_name
#   location           = var.zone
#   initial_node_count = var.node_count

#   workload_identity_config {
#     identity_namespace = "${var.project_id}.svc.id.goog"
#   }

#   node_config {
#     machine_type = var.machine_type
#     oauth_scopes = [
#       "https://www.googleapis.com/auth/cloud-platform",
#     ]
#   }

#   remove_default_node_pool = true

#   lifecycle {
#     create_before_destroy = true
#   }

#   deletion_protection = false
# }

# resource "google_container_node_pool" "primary_nodes" {
#   cluster    = google_container_cluster.primary.name
#   location   = var.zone
#   name       = "primary-node-pool"
#   node_count = var.node_count

#   node_config {
#     preemptible  = false
#     machine_type = var.machine_type
#     oauth_scopes = [
#       "https://www.googleapis.com/auth/cloud-platform",
#     ]

#     workload_metadata_config {
#       node_metadata = "GKE_METADATA_SERVER"
#     }
#   }
# }



# output "cluster_name" {
#   value = google_container_cluster.primary.name
# }

# output "endpoint" {
#   value = google_container_cluster.primary.endpoint
# }

# output "master_version" {
#   value = google_container_cluster.primary.master_version
# }
