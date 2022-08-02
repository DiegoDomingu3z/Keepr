<template>
  <div class="container">
    <div class="row">
      <div class="col-md-6 p-0">
        <img class="img-fluid img-keep" :src="keep.img" alt="" />
      </div>
      <div class="col-md-6">
        <div class="text-center pt-3 fs-5">
          <i title="Total Views" class="mdi mdi-eye-outline text-info pe-1"></i
          >{{ keep.views
          }}<i
            title="Total Kept's"
            class="mdi mdi-book-outline text-info ps-5 pe-1"
          ></i
          >{{ keep.kept }}
        </div>
        <div class="text-center pt-4">
          <h1>
            {{ keep.name }}
          </h1>
        </div>
        <div class="text-center description scrollable-y">
          <div class="pb-3 mx-3 border-1 border-bottom border-secondary">
            {{ keep.description }}
          </div>
        </div>
        <div class="d-flex justify-content-between">
          <div class="">
            <div class="dropdown">
              <button
                title="Add to vault"
                class="btn btn-info dropdown-toggle"
                type="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                Add to Vault
              </button>
              <ul class="dropdown-menu">
                <li v-for="v in vault" :key="v.id">
                  <a
                    @click="createVaultKeep({ keepId: keep.id, vaultId: v.id })"
                    class="dropdown-item"
                    href="#"
                    >{{ v.name }}</a
                  >
                </li>
              </ul>
            </div>
          </div>
          <div v-if="keep.creator?.id == account.id">
            <i
              title="Delete Keep"
              @click="deleteKeep(keep.id)"
              class="mdi mdi-delete-outline text-danger fs-3 selectable"
            ></i>
          </div>

          <div class="pt-2">
            <img
              title="Go To Profile"
              @click="goToProfile"
              class="img-fluid img-profile selectable"
              :src="keep.creator?.picture"
              alt=""
            />
            <span class="ms-2">{{ keep.creator?.name }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
import { useRoute, useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  setup() {
    const router = useRouter()
    return {
      async createVaultKeep(data) {
        try {
          await vaultKeepsService.create(data)
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async deleteKeep(id) {
        try {
          if (await Pop.confirm("This action is irreversible")) {
            await keepsService.deleteKeep(id)
          }
          Modal.getOrCreateInstance(document.getElementById("active-keep")).hide();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      goToProfile() {
        router.push({ name: "Profile", params: { id: AppState.activeKeep.creator.id } })
        Modal.getOrCreateInstance(document.getElementById("active-keep")).hide();
      },
      keep: computed(() => AppState.activeKeep),
      vault: computed(() => AppState.myVaults),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.description {
  min-height: 330px;
  max-height: 330px;
}

.img-keep {
  width: 100%;
  object-fit: cover;
  height: 500px;
}

.img-profile {
  height: 30px;
  width: 30px;
}
</style>