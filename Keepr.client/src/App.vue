<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <div class="fab-container">
    <div class="fab fab-icon">
      <i class="mdi mdi-plus fs-3"></i>
    </div>
    <ul class="fab-options">
      <li>
        <span class="fab-label">Create Keep</span>
        <div
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#create-keep"
          class="fab-icon"
        >
          <i class="mdi mdi-plus"></i>
        </div>
      </li>
      <li>
        <span class="fab-label">Create Vault</span>
        <div
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#vault"
          class="fab-icon"
        >
          <i class="mdi mdi-plus"></i>
        </div>
      </li>
    </ul>
  </div>
  <CreateKeepModal />
  <VaultModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.fab-container {
  position: fixed;
  bottom: 1em;
  right: 2em;
  z-index: 999;
  cursor: pointer;
}

.fab-icon {
  width: 70px;
  height: 70px;
  background: #55efc4;
  border-radius: 100%;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.2);
}

.fab-icon:hover {
  opacity: 0.8;
}

.fab-icon i {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  font-style: 50px;
  color: white;
}
.fab {
  width: 75px;
  height: 75px;
  background: #55efc4;
}

.fab-options {
  list-style-type: none;
  margin: 0;
  position: absolute;
  bottom: 100px;
  right: 0;
  opacity: 0;
  transition: all 0.3s ease;
  transform: scale(0);
  transform-origin: 85% bottom;
}

.fab:hover + .fab-options,
.fab-options:hover {
  opacity: 1;
  transform: scale(1);
}

.fab-options li {
  display: flex;
  justify-content: flex-end;
  padding: 5px;
}

.fab-label {
  padding: 3px 7px;
  align-self: center;
  user-select: none;
  white-space: nowrap;
  font-size: 18px;
  background: rgba(0, 0, 0, 0.65);
  color: white;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.2);
  margin-right: 10px;
}
</style>
