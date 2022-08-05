<template>
  <div
    class="modal fade half-black"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
    id="vault"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content half-black border-2 border-info">
        <div class="modal-header bg text-dark">
          <h3 class="modal-title m-2">New Vault</h3>

          <button
            type="button"
            class="btn-close btn btn-light bg-light"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body m-3">
          <form @submit.prevent="createVault" id="vault-form">
            <div class="row">
              <div class="col-md-12">
                <label for="title">Title</label>
                <input
                  required="true"
                  class="form-control bg-white"
                  placeholder="Title.."
                  type="text"
                  v-model="vaultData.name"
                />
              </div>
              <div class="col-md-12 mt-4">
                <label for="Img">Image Url</label>
                <input
                  required="true"
                  class="form-control bg-white"
                  placeholder="URL..."
                  type="text"
                  v-model="vaultData.img"
                />
              </div>
              <div class="col-md-12 mt-4">
                <textarea
                  required="true"
                  class="w-100 form-control bg-white"
                  placeholder="Description..."
                  name=""
                  id=""
                  rows="4"
                  v-model="vaultData.description"
                ></textarea>
              </div>
              <div class="pt-2 d-flex">
                <input
                  type="checkbox"
                  name=""
                  id=""
                  v-model="vaultData.isPrivate"
                />
                <span class="ps-2">Private?</span>
              </div>
              <p class="fw-lighter text-secondary">
                Private vaults can only be seen by you
              </p>
            </div>
            <div class="text-end">
              <button type="submit" class="btn btn-info">Create</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
export default {
  setup() {
    const vaultData = ref({})
    const router = useRouter()
    return {
      vaultData,
      async createVault() {
        try {
          await vaultsService.createVault(vaultData.value)
          Modal.getOrCreateInstance(document.getElementById("vault")).hide();
          document.getElementById("vault-form").reset();
          router.push({ name: "Profile", params: { id: AppState.account.id } })
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>