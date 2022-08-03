<template>
  <div class="container">
    <form @submit.prevent="editAccount" action="">
      <div class="row text-center mt-5 bg-white elevation-5">
        <div class="col-md-6 pt-5">
          <label for=""><h4>Update Name</h4></label>
          <input
            class="form-control"
            type="text"
            placeholder="Name.."
            v-model="accountData.name"
          />
        </div>
        <div class="col-md-6 pt-5 pb-5">
          <label for=""><h4>Update Profile Img</h4></label>
          <input
            class="form-control"
            type="text"
            placeholder="URL..."
            v-model="accountData.picture"
          />
        </div>
        <div class="text-end pb-3">
          <button type="submit" class="btn btn-info elevation-2">Update</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { accountService } from '../services/AccountService'
export default {
  name: 'Account',
  setup() {
    const accountData = ref({})
    watchEffect(async () => {
      try {
        accountData.value = AppState.account
      } catch (error) {

      }
    })

    return {
      accountData,
      async editAccount() {
        try {
          await accountService.editAccount(accountData.value)
          Pop.toast("Updated")
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

<style scoped>
img {
  max-width: 100px;
}
</style>
