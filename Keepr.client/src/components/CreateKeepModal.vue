<template>
  <div
    class="modal fade half-black"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
    id="create-keep"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content half-black border-2 border-info">
        <div class="modal-header bg text-dark">
          <h5 class="modal-title">New Keep</h5>
          <button
            type="button"
            class="btn-close btn btn-light bg-light"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form action="" @submit.prevent="createKeep" id="keep-form">
            <div class="row">
              <div class="col-md-12 pt-2">
                <label for="name">Name</label>
                <input
                  type="text"
                  class="form-control bg-white"
                  placeholder="Name..."
                  v-model="keepData.name"
                />
              </div>
              <div class="col-md-12 pt-4">
                <label for="img">Image Url</label>
                <input
                  type="text"
                  class="form-control bg-white"
                  placeholder="Url..."
                  v-model="keepData.img"
                />
              </div>
              <div class="col-md-12 pt-4">
                <label for="img">Image Url</label>
                <textarea
                  name="description"
                  class="w-100 form-control bg-white"
                  rows="5"
                  placeholder="Description..."
                  v-model="keepData.description"
                ></textarea>
              </div>
            </div>
            <div class="text-end pt-3">
              <button type="submit" class="btn btn-info">Create</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>




<script>
import { ref } from '@vue/reactivity'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const keepData = ref({})
    return {
      keepData,
      async createKeep() {
        try {
          await keepsService.createKeep(keepData.value)
          Modal.getOrCreateInstance(document.getElementById('create-keep')).hide();
          document.getElementById("keep-form").reset();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, "Oh no! Something went wrong")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>