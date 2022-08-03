<template>
  <div class="container">
    <div class="row">
      <div
        class="
          col-md-12
          m-3
          me-0
          mb-0
          pt-5
          phone-view
          d-flex
          justify-content-between
        "
      >
        <h1>{{ vault.name }}</h1>
        <button
          v-if="vault.creatorId == account.id"
          title="Delete Vault"
          @click="deleteVault"
          class="btn btn-danger px-4"
        >
          Delete Vault
        </button>
      </div>
      <div class="m-3 me-0 phone-view">
        <h2>Keeps: {{ vaultKeeps.length }}</h2>
      </div>
    </div>
    <div class="masonry-frame mt-5">
      <div v-for="v in vaultKeeps" :key="v.id">
        <div @click="setActive(v.id)" class="pictures keep-img keep">
          <button
            v-if="vault.creatorId == account.id"
            title="Delete Vault Keep"
            @click.stop="deleteVaultKeep(v.vaultKeepId)"
            class="btn btn-info keep-button"
          >
            <i class="mdi mdi-trash-can-outline"></i>
          </button>
          <img
            class="img-fluid rounded selectable m-2 elevation-2"
            :src="v.img"
            alt=""
          />
          <div class="keep-name text-white fs-4">{{ v.name }}</div>
          <div class="keep-creator">
            <img
              class="img-fluid creator-img selectable"
              :src="v.creator.picture"
              alt=""
            />
          </div>
        </div>
      </div>
    </div>
  </div>
  <Modal id="active-keep">
    <template #body><VaultKeepModal /> </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    return {
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(route.params.id)
            router.push({ name: "Profile", params: { id: AppState.account.id } })
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async deleteVaultKeep(id) {
        try {
          if (await Pop.confirm()) {
            await vaultKeepsService.deleteVaultKeep(id)
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async setActive(id) {
        try {
          await keepsService.setActive(id)
          Modal.getOrCreateInstance(document.getElementById("active-keep")).show();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },

      vaultKeeps: computed(() => AppState.activeVaultKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
@media (max-width: 768px) {
  .phone-view {
    margin: 0 !important;
  }
}

.masonry-frame {
  columns: 4 200px;

  div {
    break-inside: avoid;
  }
}

.pictures:hover {
  transition: 300ms;
  transform: scale(1.035);
}

.keep-img {
  position: relative;
}

.keep-name {
  position: absolute;
  z-index: 100;
  left: 0.7em;
  bottom: 0.6em;
}
.keep-creator {
  position: absolute;
  z-index: 100;
  right: 0.1em;
  bottom: 0.9em;
}
.keep-button {
  border-radius: 50%;
  position: absolute;
  z-index: 100;
  left: 0.3em;
  top: 0.1em;
  height: 35px;
  width: 35px;
  display: flex;
  justify-content: center;
}

.creator-img {
  height: 30px;
  width: 30px;
  border-radius: 50%;
}
</style>