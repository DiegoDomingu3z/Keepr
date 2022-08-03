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
        <div class="text-center">
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
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  setup() {
    return {
      async deleteVaultKeep(id) {
        try {
          if (await Pop.confirm()) {
            logger.log(AppState.activeVaultKeeps)
            await vaultKeepsService.deleteVaultKeep(id)
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      vault: computed(() => AppState.activeVault),
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      vaultKeep: computed(() => AppState.activeVaultKeeps)
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